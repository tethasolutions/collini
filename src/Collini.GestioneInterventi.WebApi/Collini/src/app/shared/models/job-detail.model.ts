import { AddressModel } from './address.model';
import { CustomerModel } from './customer.model';
import { ProductTypeModel } from './product-type.model';
import { JobSourceModel } from './job-source.model';
import { JobStatusEnum } from '../enums/job-status.enum';
import { ActivityModel } from './activity.model';

export class JobDetailModel {
    id: number;
    code:string;
    description: string;
    resultNote: string;
    operatorId: number;
    jobDate: Date;
    expirationDate: Date;
    customerId: number;
    customer: CustomerModel;
    customerName: string;
    customerAddressId: number;
    customerAddress: AddressModel;
    customerFullAddress: string;
    sourceId: number;
    source: JobSourceModel;
    productTypeId: number;
    productType: ProductTypeModel;
    status: JobStatusEnum;
    hasNotes: boolean;
    activityStart: Date;
    activityEnd: Date;
    activityOperator: string;
    isPaid: boolean;

    get expired(): boolean {
        const today = new Date();
        today.setHours(0, 0, 0, 0);

        const expiration = new Date(this.expirationDate);
        expiration.setHours(0, 0, 0, 0);

        return today > expiration;
    }
    
    constructor() {
        this.id = null;
        this.description = null;
        this.resultNote = null;
        this.operatorId = null;
        this.jobDate = new Date((new Date()).getTime());
        this.expirationDate = new Date((new Date()).getTime() + (1000 * 60 * 60 * 24 * 2));
        this.customerId = null;
        this.customer = new CustomerModel();
        this.customerName = null;
        this.customerAddressId = null;
        this.customerAddress = null;
        this.customerFullAddress = null;
        this.sourceId = null;
        this.source = new JobSourceModel();
        this.productTypeId = null;
        this.productType = new ProductTypeModel();
        this.status = JobStatusEnum.Pending;
        this.hasNotes = false;
        this.isPaid = false;
    }
}

export class JobActivitiesModel {
    readonly id: number;
    readonly activities: ActivityModel[];
    
    constructor() {
        this.id = null;
        this.activities = [];
    }
}