import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ComboNote} from "../../models";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-combo-note',
  templateUrl: './combo-note.component.html',
  styleUrls: ['./combo-note.component.css']
})
export class ComboNoteComponent {
  @Input()
  public comboNote: ComboNote = {
    noteContext: "",
    longestStreak: 0,
    footageUrl: "",
    comboString: "",
    characterId: ""
  };

  public displayVideoFlag: boolean = false;
  public videoSafeUrl: any = null;

  @Output()
  public saveComboNote: EventEmitter<any> = new EventEmitter<any>();

  @Output()
  public deleteComboNote: EventEmitter<any> = new EventEmitter<any>();

  constructor(private sanitizer: DomSanitizer) {
  }

  public onSaveComboNoteClick(): void {
    this.saveComboNote.emit()
  }

  public onDeleteComboNoteClick(): void {
    this.deleteComboNote.emit();
  }

  public toggleShowVideo(): void {
    this.videoSafeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.comboNote.footageUrl);

    this.displayVideoFlag = !this.displayVideoFlag;
  }
}
