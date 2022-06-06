import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';
import { MyvalidationService } from 'src/app/services/myvalidation.service';
import { ProductService } from 'src/app/services/product.service';
@Component({
  selector: 'app-admin-product-add',
  templateUrl: './admin-product-add.component.html',
  styleUrls: ['./admin-product-add.component.css'],
})
export class AdminProductAddComponent implements OnInit {
  productForm!: FormGroup;
  categories!: Category[];
  success!: boolean;
  loading!: boolean;
  info!: string;
  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    public myValidationService: MyvalidationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getCategory();
    this.productForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(400),
      ]),
      price: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.max(Number.MAX_SAFE_INTEGER),
        Validators.pattern('^[0-9]*$'),
      ]),
      description: new FormControl('', [
        Validators.required,
        Validators.minLength(50),
        Validators.maxLength(400),
      ]),
      imageUrl: new FormControl('', Validators.required),
      stockQuantity: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.max(Number.MAX_SAFE_INTEGER),
        Validators.pattern('^[0-9]*$'),
      ]),
      categoryId: new FormControl('', Validators.required),
    });
  }

  get getControls() {
    return this.productForm.controls;
  }

  onSubmit() {
    if (this.productForm.valid) {
      this.loading = true;
      this.productService.addProduct(this.productForm.value).subscribe(
        (result) => {
          this.success = true;
          this.router.navigateByUrl('/admin/urun/liste');
        },
        (error) => {
          this.success = false;
          this.info = 'bir hata meydana geldi ' + error.message;
          console.log(error.message);
        }
      );
    }
  }

  getCategory() {
    this.categoryService.getCategories().subscribe((result) => {
      this.categories = result.data;
    });
  }
}
