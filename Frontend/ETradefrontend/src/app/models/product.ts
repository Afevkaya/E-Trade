import { Category } from "./category";

export class Product {
  id!:number;
  name!:string;
  price!:number;
  description!:string;
  imageUrl!:string;
  stockQuantity!:number;
  categoryId!:number;
  category!:Category;
}
