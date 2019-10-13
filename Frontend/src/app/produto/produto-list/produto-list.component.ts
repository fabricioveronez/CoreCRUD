import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { ProdutoService } from '../services/produto.service';
import { Produto } from '../services/produto.model';

@Component({
  selector: 'app-produto-list',
  templateUrl: './produto-list.component.html',
  styleUrls: ['./produto-list.component.css']
})
export class ProdutoListComponent implements OnInit {

  displayedColumns: string[] = ['delete', 'nome', 'preco', 'categoria', 'descricao'];
  produtos: MatTableDataSource<Produto> = new MatTableDataSource<Produto>();

  constructor(private service: ProdutoService,
              private router: Router) { }

  public async ngOnInit() {

    await this.carregarTabela();
  }

  public onEdit(event): void {
    this.router.navigate(['/pages/produto/edit', event.data.id]);
  }

  public onCreate(): void {
    this.router.navigate(['/pages/produto/edit']);
  }

  public async onDelete(produto: Produto): Promise<any> {

    try {
      this.service.delete(produto.id);
      await this.carregarTabela();
      alert('Produto excluido com sucesso.');
    } catch (error) {
      alert('Erro ao excluir o produto.');
    }
  }

  public async carregarTabela(): Promise<any> {

    try {

      const itens = await this.service.getAll();
      this.produtos.data = itens;
    } catch (error) {
      alert('Erro ao carregar os produtos.');
    }
  }
}
