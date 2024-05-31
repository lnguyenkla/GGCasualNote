import { TestBed } from '@angular/core/testing';

import { ComboNoteService } from './combo-note.service';

describe('ComboNoteService', () => {
  let service: ComboNoteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComboNoteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
