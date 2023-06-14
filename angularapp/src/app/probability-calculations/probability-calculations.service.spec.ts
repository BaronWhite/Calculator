import { TestBed, fakeAsync } from '@angular/core/testing';

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProbabilityCalculationType } from '../interfaces/probability-calculation-type';
import { ProbabilityCalculationsService } from './probability-calculations.service';

describe('ProbabilityCalculationsService', () => {
  let service: ProbabilityCalculationsService;
  let http: HttpTestingController;
  let httpClient: HttpClient;

  const errorResponse = new HttpErrorResponse({
    error: { code: `ZYX`, message: `Meh.` },
    status: 400,
    statusText: 'Bad Request',
  });

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(ProbabilityCalculationsService);
    http = TestBed.inject(HttpTestingController);
    httpClient = TestBed.inject(HttpClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getSupportedCalculations', () => {
    it('should return response', fakeAsync(() => {
      let response: ProbabilityCalculationType[] = [
        {
          display: 'Multiply',
          description: '(A)(B)',
          calculationType: 0,
        },
        {
          display: 'Divide',
          description: '(A)/(B)',
          calculationType: 1,
        }
      ];
      service.getSupportedCalculations().then(
        value => {
          expect(value).toEqual(response);
        }
      );

      const req = http.expectOne('probability/supported-calculations');
      req.flush(response);
      expect(req.request.method).toEqual('GET');
      http.verify();
    }));
  });

  describe('calculate', () => {
    it('should return response', fakeAsync(() => {
      const request = {
        a: 0.5,
        b: 0.75,
        calculationType: 5,
      };
      let response: number = 0.75;

      service.calculate(request).then(
        value => {
          expect(value).toEqual(response);
        }
      );

      const req = http.expectOne('probability/calculate');
      expect(req.request.method).toEqual('POST');
      req.flush(response);
      http.verify();
    }));

    it('should handle error', fakeAsync(() => {
      const request = {
        a: 0.5,
        b: 0.75,
        calculationType: 5,
      };

      service.calculate(request).then(
        () => { },
        error => expect(error.message).toEqual('Something bad happened; please try again later.')
      );

      const req = http.expectOne('probability/calculate');
      expect(req.request.method).toEqual('POST');
      req.error(new ProgressEvent(''));
      http.verify();
    }));
  });
});
