import {Component, EventEmitter, OnInit} from '@angular/core';

export const HEADER_TIMESTAMP_UPDATE: EventEmitter<string> = new EventEmitter<string>();


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
  public isLoading: boolean = false;
  public lastUpdated: string = "";
  public isTimestampDisplayed: boolean = false;

  constructor() {

  }

  ngOnInit() {
    HEADER_TIMESTAMP_UPDATE.subscribe((timestamp: string) => {
      this.lastUpdated = timestamp;
    });
  }
}
