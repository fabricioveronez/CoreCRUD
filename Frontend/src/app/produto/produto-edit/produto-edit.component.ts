import { ProdutoService } from './../services/produto.service';
import { Produto } from './../services/produto.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { async } from 'q';

@Component({
  selector: 'app-produto-edit',
  templateUrl: './produto-edit.component.html',
  styleUrls: ['./produto-edit.component.css']
})
export class ProdutoEditComponent implements OnInit {

  produto: Produto = new Produto();

  constructor(private service: ProdutoService,
              private route: ActivatedRoute,
              private router: Router) { }

  public ngOnInit(): void {
    this.route.paramMap.subscribe(async paramMap => {
      if (paramMap.get('id')) {

        try {
          this.produto = await this.service.get(paramMap.get('id'));
        } catch (error) {
          alert('Erro ao carregar o produto.');
        }
      } else {
        this.produto = new Produto();
      }
    });
  }

  public async salvarProduto(): Promise<any> {

    try {
      await this.service.save(this.produto);
      alert('Cadastro feito.');
      this.router.navigate(['listar']);
    } catch (error) {
      alert('Erro ao efetuar o cadastro.');
    }
  }
}
