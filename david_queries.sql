

------Obtener todas las órdenes creadas hoy (según CreatedAt).-----------------
SELECT * FROM Orders where CAST (CreatedAt AS DATE) = CAST(SYSDATETIME() AS DATE);

--------Obtener todos los exámenes de una orden específica. ------------------------

SELECT e.Id, e.Code,e.Name From OrderExams oe  INNER JOIN Exams e ON oe.ExamId = e.Id WHERE oe.orderId =1;


--------Contar cuántas órdenes tiene cada paciente, incluyendo pacientes sin órdenes.---------

SELECT p.Id,p.FullName,COUNT(o.Id) AS totalOrders FROM Patients p LEFT JOIN Orders o ON p.Id = o.PatientId GROUP BY p.Id, p.FullName ORDER BY p.FullName;