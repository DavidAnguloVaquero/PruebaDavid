import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { OrderDto, OrderService, PagedOrders } from 'src/app/services/examen.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  //variables
  mode: 'list' | 'detail' | 'create' = 'list';

  orders: any;
  currentPage = 1;
  pageSize = 5;
  totalPages = 1;

  selectedOrder: OrderDto | null = null;
  paciente: any;

  // Formulario para crear orden
  orderForm: FormGroup;

  // Constructor
  constructor(private orderService: OrderService, private fb: FormBuilder) {
    this.orderForm = this.fb.group({
      patientName: ['', [Validators.required, Validators.minLength(3)]],
      date: ['', [Validators.required, this.futureDateValidator]],
      exams: this.fb.array([], Validators.minLength(1))
    });
  }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders() {
    this.orderService.getOrdersPaged(this.currentPage, this.pageSize)
      .subscribe({
        next: (result) => {
          console.log(result);
          this.orders = result.data;
          this.totalPages = Math.ceil(result.total / this.pageSize);
        },
        error: (err) => {
          console.error('Error de conexión:', err);
          alert('No se pudo conectar con el servidor. Intenta nuevamente.');
        }
      });

  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.loadOrders();
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadOrders();
    }
  }

  //  Detalle
  showDetail(orderId: number, patient: any) {
    this.orderService.getOrderById(orderId).subscribe(order => {
      this.selectedOrder = order;
      this.paciente = patient;
      console.log(this.selectedOrder);
      console.log(this.selectedOrder.exams)
      this.mode = 'detail';
    });
  }

  backToList() {
    this.mode = 'list';
    this.selectedOrder = null;
    this.loadOrders();
  }

  get exams(): FormArray {
    return this.orderForm.get('exams') as FormArray;
  }

  addExam() {
    this.exams.push(this.fb.group({
      code: ['', Validators.required],
      name: ['', Validators.required]
    }));
  }

  removeExam(index: number) {
    this.exams.removeAt(index);
  }

  submitOrder() {
    if (this.orderForm.invalid) {
      this.orderForm.markAllAsTouched();
      return;
    }

    this.orderService.createOrder(this.orderForm.value).subscribe(() => {
      alert('Orden creada correctamente');
      this.orderForm.reset();
      this.exams.clear();
      this.mode = 'list';
      this.loadOrders();
    });
  }

  // Validator
  futureDateValidator(control: any) {
    const today = new Date();
    const inputDate = new Date(control.value);
    return inputDate > today ? { futureDate: true } : null;
  }
}
