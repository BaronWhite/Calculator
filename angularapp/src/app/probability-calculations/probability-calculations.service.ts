import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, lastValueFrom, throwError } from 'rxjs';
import { ProbabilityCalculation } from '../interfaces/probability-calculation';
import { ProbabilityCalculationType } from '../interfaces/probability-calculation-type';

@Injectable({
  providedIn: 'root'
})
export class ProbabilityCalculationsService {

  constructor(private http: HttpClient) { }

  getSupportedCalculations(): Promise<ProbabilityCalculationType[]> {
    return lastValueFrom(this.http.get<ProbabilityCalculationType[]>('probability/supported-calculations')
      .pipe(
        catchError(this.handleError)
      ));
  }

  calculate(calculation: ProbabilityCalculation): Promise<number> {
    return lastValueFrom(this.http.post<number>('probability/calculate', calculation)
      .pipe(
        catchError(this.handleError)
      ));
  }

  // this is from docs, I'd implement a notificatio system with toasts or popups
  // and react to it in the component
  // ideally the server would return errors in an expected format for e.g. validation errors
  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
