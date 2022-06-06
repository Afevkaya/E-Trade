import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { Product } from 'src/app/models/product';
import { CategoryService } from 'src/app/services/category.service';
import { MyvalidationService } from 'src/app/services/myvalidation.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-admin-product-update',
  templateUrl: './admin-product-update.component.html',
  styleUrls: ['./admin-product-update.component.css'],
})
export class AdminProductUpdateComponent implements OnInit {
  productForm!: FormGroup;
  product!: Product;
  categories!: Category[];
  success!: boolean;
  loading!: boolean;
  info!: string;
  productId!: number;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    public myValidationService: MyvalidationService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getCategory();
    this.productId = Number(this.route.snapshot.paramMap.get('id'));
    this.productService.getProduct(this.productId).subscribe((result) => {
      this.productForm.controls['name'].setValue(result.data.name);
      this.productForm.controls['price'].setValue(result.data.price);
      this.productForm.controls['imageUrl'].setValue(result.data.imageUrl);
      this.productForm.controls['stockQuantity'].setValue(
        result.data.stockQuantity
      );
      this.productForm.controls['description'].setValue(
        result.data.description
      );
      this.productForm.controls['categoryId'].setValue(result.data.categoryId);
    });

    this.productForm = new FormGroup({
      name: new FormControl([
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(String.length),
      ]),
      price: new FormControl([
        Validators.required,
        Validators.min(1),
        Validators.max(Number.MAX_SAFE_INTEGER),
        Validators.pattern('^[0-9]*$'),
      ]),
      description: new FormControl([
        Validators.required,
        Validators.minLength(50),
        Validators.maxLength(String.length),
      ]),
      imageUrl: new FormControl(Validators.required),
      stockQuantity: new FormControl( [
        Validators.required,
        Validators.min(1),
        Validators.max(Number.MAX_SAFE_INTEGER),
        Validators.pattern('^[0-9]*$'),
      ]),
      categoryId: new FormControl(Validators.required),
    });
  }

  onSubmit() {
    if (this.productForm.valid) {
      this.loading = true;
      this.product = new Product();
      this.product.id = this.productId;
      this.product.name = this.productForm.get('name')?.value;
      this.product.price = this.productForm.get('price')?.value;
      this.product.description = this.productForm.get('description')?.value;
      this.product.imageUrl = this.productForm.get('imageUrl')?.value;
      this.product.stockQuantity = this.productForm.get('stockQuantity')?.value;
      this.product.categoryId = this.productForm.get('categoryId')?.value;
      this.productService
        .updateProduct(this.product)
        .subscribe((result) => {
          this.success = true;
          this.router.navigateByUrl('/admin/urun/liste');
        });
    }
  }

  getCategory() {
    this.categoryService.getCategories().subscribe(
      (result) => {
        this.categories = result.data;
      },
      (error) => {
        this.success = false;
        this.info = 'bir hata meydana geldi ' + error.message;
        console.log(error.message);
      }
    );
  }
}
