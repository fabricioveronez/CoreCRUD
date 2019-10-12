import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Produto } from './produto.model';

@Injectable({
  providedIn: 'root',
})
export class ProdutoService {

  constructor(private http: HttpClient) { }

  public getAll(): Promise<Produto[]> {
    return this.http.get<Produto[]>(`${environment.apiURL}/api/produto`).toPromise();
  }

  public get(id: string): Promise<Produto> {
    return this.http.get<Produto>(`${environment.apiURL}/api/produto/${id}`).toPromise();
  }

  public delete(id: string): Promise<object> {
    return this.http.delete(`${environment.apiURL}/api/produto/${id}`).toPromise();
  }

  public save(produto: Produto): Promise<object> {

    if (produto.id) {
      return this.http.put(`${environment.apiURL}/api/produto/${produto.id}`, produto).toPromise();
    } else {
      return this.http.post(`${environment.apiURL}/api/produto`, produto).toPromise();
    }
  }
}
