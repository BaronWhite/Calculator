import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor() {
    // http.get<WeatherForecast[]>('/weatherforecast').subscribe(result => {
    //   this.forecasts = result;
    // }, error => console.error(error));
  }
}
