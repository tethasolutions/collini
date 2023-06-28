import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoteAttachmentsModalComponent } from './note-attachments-modal.component';

describe('NoteAttachmentsModalComponent', () => {
  let component: NoteAttachmentsModalComponent;
  let fixture: ComponentFixture<NoteAttachmentsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoteAttachmentsModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NoteAttachmentsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
