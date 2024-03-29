import { AddressModel } from './address.model';
import { ContactTypeEnum } from '../enums/contact-type.enum';
import { ContactFiscalTypeEnum } from '../enums/contact-fiscal-type.enum';

export class CustomerModel {
    id: number;
    type: ContactTypeEnum;
    companyName: string;
    name: string;
    surname: string;
    fiscalType: ContactFiscalTypeEnum;
    erpCode: string;
    alert: boolean;
    addresses: AddressModel[];

    mainAddress: AddressModel;
    telephone: string;
    email: string;

    customerDescriptionLower:string;
    
    description:string = this.customerDescription;

    get customerDescription(): string {
        let result = '';
        if (this.companyName !== null) { result += `${this.companyName} `; }        
        if (this.surname !== null) { result +=  `${this.surname} `; }
        if (this.name !== null) { result +=  `${this.name}`; }
        return result;
    }

    get customerDescriptionWithAddress(): string {
        let result = '';
        if (this.companyName !== null) { result += `${this.companyName} `; }        
        if (this.surname !== null) { result +=  `${this.surname} `; }
        if (this.name !== null) { result +=  `${this.name} `; }
        if (this.mainAddress !== null) { result += `${this.mainAddress.fullAddress}`}
        return result;
    }

    get typeDescription(): string {
        if (this.type >= 0) { return ContactTypeEnum[this.type]; }
        else { return ''; }
    }

    get fiscalTypeDescription(): string {
        if (this.fiscalType >= 0) { return ContactFiscalTypeEnum[this.fiscalType]; }
        else { return ''; }
    }

    constructor() {
        this.id = null;
        this.type = null;
        this.companyName = null;
        this.name = null;
        this.surname = null;
        this.fiscalType = null;
        this.erpCode = null;
        this.alert = false;
        this.addresses = [];

        this.mainAddress = null;
        this.telephone = null;
        this.email = null;
    }
}
