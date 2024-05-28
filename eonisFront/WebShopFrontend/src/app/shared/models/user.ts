import { IOrder } from "./order"

export interface IUser {
    token: string
    email: string
    role?: string
    name?: string
    city?: string
    streetAndNumber?: string
    postalCode?: string
    orders?: IOrder[]

  }
  