import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdutoEditComponent } from './produto-edit.component';

describe('ProdutoEditComponent', () => {
  let component: ProdutoEditComponent;
  let fixture: ComponentFixture<ProdutoEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProdutoEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProdutoEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
