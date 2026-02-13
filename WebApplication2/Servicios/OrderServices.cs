using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Servicios
{
    public class OrderServices : IPruebaServices
    {
        private readonly IthealthContext _context;
        private readonly DbSet<OrderExam> _dbSet;

        public OrderServices(IthealthContext context)
        {
            _context = context;
            _dbSet = _context.Set<OrderExam>();
        }

        //public async Task<object> CreateOrderAsync2(CreateOrderRequest request)
        //{
        //    if (request.AttentionDate > DateTime.Now)
        //        throw new Exception("la fecha de atencion no puede ser futura");


        //    if (request.PatientName.Length < 3)
        //        throw new Exception("El nombre del paciente debe tener mínimo 3 caracteres.");

        //    if (request.Exams == null || !request.Exams.Any())
        //        throw new Exception("Debe incluir al menos un examen.");


        //    var patient = await _context.Patients.FromSqlRaw("Select * from Patiens Where FullName ={0}", request.PatientName).FirstOrDefaultAsync();

        //    if (patient == null)
        //    {
        //        await _context.Database.ExecuteSqlRawAsync(
        //            "INSERT INTO Patients (FullName, CreatedAt) VALUES ({0}, GETDATE())",
        //            request.PatientName);

        //        patient = await _context.Patients
        //            .FromSqlRaw("SELECT TOP 1 * FROM Patients WHERE FullName = {0} ORDER BY Id DESC", request.PatientName)
        //            .FirstOrDefaultAsync();
        //    }

        //    //crear Odden

        //    await _context.Database.ExecuteSqlRawAsync(
        //        "INSERT INTO Orders (PatientId, AttentionDate, CreatedAt) VALUES ({0}, {1}, GETDATE())",
        //        patient.Id, request.AttentionDate);


        //    var order = await _context.Orders.FromSqlRaw("SELECT TOP 1 * FROM Orders WHERE PatientId = {0} ORDER BY Id DESC", patient.Id)
        //        .FirstOrDefaultAsync();



        //    foreach (var examDto in request.Exams)
        //    {
        //        var exam = await _context.Exams
        //            .FromSqlRaw("SELECT * FROM Exams WHERE Code = {0}", examDto.Code)
        //            .FirstOrDefaultAsync();

        //        if (exam == null)
        //        {
        //            await _context.Database.ExecuteSqlRawAsync(
        //                "INSERT INTO Exams (Code, Name) VALUES ({0}, {1})",
        //                examDto.Code, examDto.Name);

        //            exam = await _context.Exams
        //                .FromSqlRaw("SELECT TOP 1 * FROM Exams WHERE Code = {0} ORDER BY Id DESC", examDto.Code)
        //                .FirstOrDefaultAsync();
        //        }

        //        await _context.Database.ExecuteSqlRawAsync(
        //            "INSERT INTO OrderExams (OrderId, ExamId) VALUES ({0}, {1})",
        //            order.Id, exam.Id);
        //    }

        //    return new
        //    {
        //        order.Id,
        //        PatientName = patient.FullName,
        //        order.AttentionDate,
        //        CreatedAt = order.CreatedAt
        //    };


        //}

        public async Task<object> CreateOrderAsync(CreateOrderRequest request)
        {
            if (request.AttentionDate > DateTime.Now)
                throw new Exception("La fecha de atención no puede ser futura");

            if (string.IsNullOrWhiteSpace(request.PatientName) || request.PatientName.Length < 3)
                throw new Exception("El nombre del paciente debe tener mínimo 3 caracteres.");

            if (request.Exams == null || !request.Exams.Any())
                throw new Exception("Debe incluir al menos un examen.");

            // 1 Buscar o crear paciente
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.FullName == request.PatientName);

            if (patient == null)
            {
                patient = new Patient { FullName = request.PatientName };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync(); // EF Core asigna Id automáticamente
            }

            //  Crear orden
            var order = new Order
            {
                PatientId = patient.Id,
                AttentionDate = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // EF Core asigna Id automáticamente

            //Agregar exámenes a la orden
            foreach (var examDto in request.Exams)
            {
                // Buscar examen
                var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Code == examDto.Code);

                // Si no existe, crear
                if (exam == null)
                {
                    exam = new Exam
                    {
                        Code = examDto.Code,
                        Name = examDto.Name
                    };
                    _context.Exams.Add(exam);
                    await _context.SaveChangesAsync();
                }

              
                order.Exams.Add(exam);
            }

            // Guardar cambios
            await _context.SaveChangesAsync();

            // Retornar resultado
            return new
            {
                order.Id,
                PatientName = patient.FullName,
                order.AttentionDate,
                order.CreatedAt
            };
        }



        public async Task<object> GetOrdersPagedAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;

            var query = _context.Orders
                .Include(o => o.Patient) // Traer datos del paciente
                .Include(o => o.Exams)   // Traer exámenes relacionados
                .OrderByDescending(o => o.CreatedAt);

            // Total de registros
            var total = await query.CountAsync();

            // Obtener página actual
            var data = await query
                .Skip(skip)
                .Take(pageSize)
                .Select(o => new
                {
                    o.Id,
                    PatientName = o.Patient.FullName,
                    o.AttentionDate,
                    o.CreatedAt,
                    ExamsCount = o.Exams.Count
                })
                .ToListAsync();

            return new
            {
                Total = total,
                Page = page,
                PageSize = pageSize,
                Data = data
            };
        }



        public async Task<object> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.FromSqlRaw(@"SELECT o.Id, o.PatientId, o.AttentionDate, o.CreatedAt FROM Orders oWHERE o.Id = {0}", id).FirstOrDefaultAsync();

            if (order == null)
                return null;

            var exams = await _context.Exams
                .FromSqlRaw(@"SELECT e.Id, e.Code, e.Name FROM Exams e
                              INNER JOIN OrderExams oe ON e.Id = oe.ExamId
                              WHERE oe.OrderId = {0}", id)
                .ToListAsync();

            return new
            {
                order.Id,
                order.Patient,
                order.AttentionDate,
                order.CreatedAt,
                Exams = exams
            };
        }





    }
}
