import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { HttpClient, HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { environment } from 'src/environments/environment';
import { TableApi } from '../api/tableApi';
import { CurrentpageService } from '../services/currentpage.service';
import { TableComponent } from '../table/table.component';
import { BaseFile, Dir } from '../table/table/fileinfo';
import { NewdirComponent } from './newdir/newdir.component';
import { map } from 'rxjs/operators';
import { UploadFile, UploadService } from '../services/upload.service';
import { UploadComponent } from './upload/upload.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.sass']
})
export class ToolbarComponent implements OnInit {
  newDirName: string;
  @Input() table: TableComponent;

  @Output() collapse = new EventEmitter<boolean>();

  isHandset: boolean = false;

  constructor(public breakobs: BreakpointObserver,
    public cur: CurrentpageService,
    public dialog: MatDialog,
    public uploadServ: UploadService,
    public http: HttpClient) {
  }

  ngOnInit(): void {
    this.breakobs.observe([Breakpoints.HandsetLandscape, Breakpoints.HandsetPortrait]).subscribe(res => {
      this.isHandset = res.matches;
      this.collapse.emit(this.isHandset);
    });
  }

  sidernavToggle() {
    this.collapse.emit();
  }

  addNewFiles() {
    const newdia = this.dialog.open(UploadComponent, { minWidth: 350 });
    newdia.afterClosed().subscribe(res => {
      this.table.justSetableData(this.cur.getCurrentPage());
    });
  }

  addNewFolder() {
    const newdia = this.dialog.open(NewdirComponent, { minWidth: 200 });
    newdia.afterClosed().subscribe((res: string) => {
      this.table.justSetableData(this.cur.getCurrentPage());
    });
  }

}
