import { IOrder } from "./order"

export interface IUser {
    token: string
    userId?: string
    email: string
    role?: string
    name?: string
    city?: string
    streetAndNumber?: string
    postalCode?: string
    orders?: IOrder[]
    contactNumber?: string
  }
  
  export interface IUpdateUserCommand {
    userId: string;
    name: string;
    contactNumber?: string;
    city?: string;
    streetAndNumber?: string;
    postalCode?: string;
}