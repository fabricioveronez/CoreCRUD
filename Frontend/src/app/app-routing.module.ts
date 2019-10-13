import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutoEditComponent } from './produto/produto-edit/produto-edit.component';
import { ProdutoListComponent } from './produto/produto-list/produto-list.component';


const routes: Routes = [
  { path: 'editar/:id', component: ProdutoEditComponent },
  { path: 'editar', component: ProdutoEditComponent },
  { path: 'listar', component: ProdutoListComponent },
  { path: '', redirectTo: '/listar',  pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
