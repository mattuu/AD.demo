import { TestBed } from '@angular/core/testing';

import { ColoursService } from './colours.service';

describe('ColoursService', () => {
  let service: ColoursService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ColoursService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
