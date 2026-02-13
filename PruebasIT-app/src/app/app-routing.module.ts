import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrdersComponent } from './components/orders/orders.component';


const routes: Routes = [
  { path: '', redirectTo: 'orders', pathMatch: 'full' }, // Redirige al listado de órdenes
  { path: 'orders', component: OrdersComponent },
  { path: '**', redirectTo: 'orders' } // Cualquier ruta desconocida
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
