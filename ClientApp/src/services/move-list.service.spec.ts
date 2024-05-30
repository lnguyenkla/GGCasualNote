import { TestBed } from '@angular/core/testing';

import { MoveListService } from './move-list.service';

describe('MoveListService', () => {
  let service: MoveListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MoveListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
