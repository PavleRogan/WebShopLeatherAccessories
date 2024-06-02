import { v4 as uuidv4 } from 'uuid';

export interface IOrder{
    orderId?: string
    orderDate?: string
    processed?: boolean
    userId?: string
    orderItems: IOrderItem[]
    clientSecret?: string;
    paymentIntendId?: string;
  }

  export interface IOrderItem {
    productId?: string
    quantity?: number
    price?: number,
    name?: string
  }

  export class Order implements IOrder{
    orderId = uuidv4();
    orderDate?: string;
    processed?: boolean;
    userId?: string = "";
    orderItems : IOrderItem[] = [];
  }
  