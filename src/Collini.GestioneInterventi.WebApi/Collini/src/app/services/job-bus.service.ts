import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class JobBusService {
    private readonly _jobUpdated = new Subject<void>();
    readonly onJobUpdated = this._jobUpdated.asObservable();

    jobUpdated() {
        this._jobUpdated.next();
    }
}

