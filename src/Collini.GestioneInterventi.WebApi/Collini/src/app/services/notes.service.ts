import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ApiUrls } from './common/api-urls';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { AddressModel } from '../shared/models/address.model';
import { NoteModel } from '../shared/models/note.model';
import { CustomerModel } from '../shared/models/customer.model';
import { NoteAttachmentModel } from '../shared/models/note-attachment.model';

@Injectable()
export class NotesService {
    
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/notes`;

    constructor(
        private readonly _http: HttpClient
    ) {}

    getJobNotes(jobId: number) {
        return this._http.get<Array<NoteModel>>(`${this._baseUrl}/job-notes/${jobId}`)
            .pipe(
                map(response => {
                    const notes = this.getNotesArray(response);
                    return notes;
                })
            );
    }

    getActivityNotes(activityId: number) {
        return this._http.get<Array<NoteModel>>(`${this._baseUrl}/activity-notes/${activityId}`)
            .pipe(
                map(response => {
                    const notes = this.getNotesArray(response);
                    return notes;
                })
            );
    }

    getNotesArray(listeNote: Array<any>): Array<NoteModel> {
        const notes: Array<NoteModel> = [];
        listeNote.forEach(item => {
            const nota = Object.assign(new NoteModel(), item);

            const operator: CustomerModel = Object.assign(new CustomerModel(), nota.operator);
            nota.operator = operator;

            nota.createdOn = new Date(nota.createdOn);

            const attachments: Array<NoteAttachmentModel> = [];
            nota.attachments.forEach((attachmentItem: any) => {
                const attachment: NoteAttachmentModel = Object.assign(new NoteAttachmentModel(), attachmentItem);
                attachments.push(attachment);
            });
            nota.attachments = attachments;

            notes.push(nota);
        });
        return notes;
    }
}
