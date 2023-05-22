import { ContactAddressModel } from './contact-address.model';
import { JobModel } from './job.model';
import { OrderModel } from './order.model';
import { ContactTypeEnum } from '../enums/contact-type.enum';
import { ContactFiscalTypeEnum } from '../enums/contact-fiscal-type.enum';

export class ContactModel {
    type: ContactTypeEnum;
    companyName: string;
    name: string;
    surname: string;
    fiscalType: ContactFiscalTypeEnum;
    erpCode: string;
    alert: boolean;
    addresses: ContactAddressModel[];
    jobs: JobModel[];
    orders: OrderModel[];
}
