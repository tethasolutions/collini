import { AddressModel } from './address.model';

export class CustomerModel {
    customerSupplierId: number;
    type: string;
    companyName: string;
    name: string;
    surname: string;
    city: string;
    address: string;
    province: string;
    zipCode: string;
    telephone: string;
    email: string;
    fiscalType: string;
    erpCode: string;
    alert: boolean;
    addresses: AddressModel[];

    constructor() {
        this.customerSupplierId = null;
        this.type = null;
        this.companyName = null;
        this.name = null;
        this.surname = null;
        this.city = null;
        this.address = null;
        this.province = null;
        this.zipCode = null;
        this.telephone = null;
        this.email = null;
        this.fiscalType = null;
        this.erpCode = null;
        this.alert = null;
        this.addresses = [];
    }
}
