import { AddressModel } from './address.model';

export class CustomerModel {
    customerSupplierId: number;
    type: string;
    companyName: string;
    name: string;
    surname: string;
    telephone: string;
    email: string;
    fiscalType: string;
    erpCode: string;
    alert: boolean;
    addresses: AddressModel[];

    mainAddress: AddressModel;

    get customerDescription(): string {
        var result = '';
        if (this.name !== null) { result +=  `${this.name} `; }
        if (this.surname !== null) { result +=  `${this.surname}`; }
        return result;
    }

    constructor() {
        this.customerSupplierId = null;
        this.type = null;
        this.companyName = null;
        this.name = null;
        this.surname = null;
        this.telephone = null;
        this.email = null;
        this.fiscalType = null;
        this.erpCode = null;
        this.alert = null;
        this.addresses = [];

        this.mainAddress = null;
    }
}
