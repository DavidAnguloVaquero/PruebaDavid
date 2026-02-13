import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ExamDto {
  code: string;
  name: string;
}

export interface CreateOrderRequest {
  patientName: string;
  attentionDate: string; 
  exams: ExamDto[];
}

export interface OrderDto {
  id: number;
  patientName: string;
  attentionDate: string;
  createdAt: string;
  exams?: { id: number; code: string; name: string }[];
}

export interface PagedOrders {
  total: number;
  page: number;
  pageSize: number;
  data: OrderDto[];
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private baseUrl = 'https://localhost:7115/api/Order';

  constructor(private http: HttpClient) {}

  createOrder(request: CreateOrderRequest): Observable<OrderDto> {
    return this.http.post<OrderDto>(`${this.baseUrl}`, request);
  }

  getOrdersPaged(page: number = 1, pageSize: number = 10): Observable<PagedOrders> {
    return this.http.get<PagedOrders>(`${this.baseUrl}/Listar?page=${page}&pageSize=${pageSize}`);
  }

  getOrderById(id: number): Observable<OrderDto> {
    return this.http.get<OrderDto>(`${this.baseUrl}/${id}`);
  }
}
