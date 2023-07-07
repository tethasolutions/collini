import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType, HttpHeaders, HttpRequest, HttpResponse } from '@angular/common/http';
import { filter, map } from 'rxjs/operators';
import { ApiUrls } from './common/api-urls';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { AddressModel } from '../shared/models/address.model';
import { NoteModel } from '../shared/models/note.model';
import { CustomerModel } from '../shared/models/customer.model';
import { NoteAttachmentModel } from '../shared/models/note-attachment.model';
import { UserModel } from '../shared/models/user.model';
import { NoteAttachmentUploadFileModel } from '../shared/models/note-attachment-upload-file.model';

@Injectable()
export class NotesService {

    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/notes`;

    constructor(
        private readonly _http: HttpClient
    ) { }

    getJobNotes(jobId: number) {
        return this._http.get<Array<NoteModel>>(`${this._baseUrl}/job-notes/${jobId}`)
            .pipe(
                map(response => {
                    const notes = this.getNotesArray(response);
                    return notes;
                })
            );
    }

    getQuotationNotes(quotationId: number) {
        return this._http.get<Array<NoteModel>>(`${this._baseUrl}/quotation-notes/${quotationId}`)
            .pipe(
                map(response => {
                    const notes = this.getNotesArray(response);
                    return notes;
                })
            );
    }

    getOrderNotes(orderId: number) {
        return this._http.get<Array<NoteModel>>(`${this._baseUrl}/order-notes/${orderId}`)
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

            const operator: UserModel = Object.assign(new UserModel(), nota.operator);
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

    getNoteDetail(id: number) {
        return this._http.get<NoteModel>(`${this._baseUrl}/note-detail/${id}`)
            .pipe(
                map(e => {
                    const nota = Object.assign(new NoteModel(), e);

                    const operator: UserModel = Object.assign(new UserModel(), nota.operator);
                    nota.operator = operator;

                    nota.createdOn = new Date(nota.createdOn);

                    const attachments: Array<NoteAttachmentModel> = [];
                    nota.attachments.forEach((attachmentItem: any) => {
                        const attachment: NoteAttachmentModel = Object.assign(new NoteAttachmentModel(), attachmentItem);
                        attachments.push(attachment);
                    });
                    nota.attachments = attachments;

                    return nota;
                })
            );
    }

    createNote(request: NoteModel) {
        return this._http.post<number>(`${this._baseUrl}/note`, request)
            .pipe(
                map(e => {
                    return e;
                })
            );
    }

    updateNote(request: NoteModel, id: number) {
        return this._http.put<void>(`${this._baseUrl}/note/${id}`, request)
            .pipe(
                map(() => { })
            );
    }

    getNoteAttachments(noteId: number) {
        return this._http.get<Array<NoteAttachmentModel>>(`${this._baseUrl}/notes-attachments/${noteId}`)
            .pipe(
                map(response => {
                    const noteAttachments: Array<NoteAttachmentModel> = [];
                    response.forEach((attachmentItem: any) => {
                        const attachment: NoteAttachmentModel = Object.assign(new NoteAttachmentModel(), attachmentItem);
                        noteAttachments.push(attachment);
                    });
                    return noteAttachments;
                })
            );
    }

    getNoteAttachmentDetail(id: number) {
        return this._http.get<NoteAttachmentModel>(`${this._baseUrl}/note-attachment-detail/${id}`)
            .pipe(
                map(e => {
                    const noteAttachment: NoteAttachmentModel = Object.assign(new NoteAttachmentModel(), e);
                    return noteAttachment;
                })
            );
    }

    createNoteAttachment(request: NoteAttachmentModel) {
        return this._http.post<number>(`${this._baseUrl}/note-attachment`, request)
            .pipe(
                map(e => {
                    return e;
                })
            );
    }

    updateNoteAttachment(request: NoteAttachmentModel, id: number) {
        return this._http.put<void>(`${this._baseUrl}/note-attachment/${id}`, request)
            .pipe(
                map(() => { })
            );
    }

    uploadNoteAttachmentFile(file: File) {
        const formData = new FormData();

        formData.append(file.name, file);

        const uploadReq = new HttpRequest("POST",
            `${this._baseUrl}/note-attachment/upload-file`,
            formData,
            {
                reportProgress: false
            });

        return this._http.request(uploadReq)
            .pipe(
                filter(e => e.type === HttpEventType.Response),
                map(e => (e as HttpResponse<NoteAttachmentUploadFileModel>).body),
                map(e => new NoteAttachmentUploadFileModel(e.fileName, e.originalFileName))
            );
    }
}
