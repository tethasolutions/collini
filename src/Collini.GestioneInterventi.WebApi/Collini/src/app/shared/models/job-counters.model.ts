import { JobCounterModel } from "./job-counter.model";

export class JobCountersModel {
    acceptance: JobCounterModel;
    actives: JobCounterModel;
    desks: JobCounterModel;
    preventives: JobCounterModel;
    supplierOrders: JobCounterModel;
    interventions: JobCounterModel;
    completed: JobCounterModel;
    billing: JobCounterModel;

    constructor() {
        this.acceptance = new JobCounterModel();
        this.actives = new JobCounterModel();
        this.desks = new JobCounterModel();
        this.preventives = new JobCounterModel();
        this.supplierOrders = new JobCounterModel();
        this.interventions = new JobCounterModel();
        this.completed = new JobCounterModel();
        this.billing = new JobCounterModel();
    }
}
