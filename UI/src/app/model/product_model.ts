export  class                                                                                                                                                 Product  {
    id : number;
    name : string;
    price : number;
    url : string;
   
    
    constructor(id : number , name : string , price : number , url : string) {
        this.name = name;
        this.price = price;
        this.url = url;
        this.id = id;
    }
}
export class ProductInBackend extends Product {
    
     amount : number;
    
    constructor(id : number , name : string , price : number , url : string,amount: number) {
        super(id , name, price, url);
        this.amount = amount;
    }

}
export interface ShoppingCartResponse {
    products: any[];  
    total_price: number;  
    
}