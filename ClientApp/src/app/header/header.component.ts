import {Component, EventEmitter} from '@angular/core';
import {MoveListService} from "../../services/move-list.service";

export const TIMESTAMP_UPDATE: EventEmitter<string> = new EventEmitter<string>();

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  public isLoading: boolean = false;
  public lastUpdated: string = "";

  constructor(private moveListService: MoveListService) {
    TIMESTAMP_UPDATE.subscribe((timestamp: string) => {
      this.lastUpdated = timestamp;
    });
  }
}
