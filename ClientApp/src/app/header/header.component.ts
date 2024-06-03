import { Component } from '@angular/core';
import {MoveListService} from "../../services/move-list.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  public isLoading: boolean = false;
  public lastUpdated: string = "";

  constructor(private moveListService: MoveListService) {
  }
}
