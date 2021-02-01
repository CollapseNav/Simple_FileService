import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Platform } from '@angular/cdk/platform';
import { Component, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { environment } from 'src/environments/environment';
import { TableApi } from './api/tableApi';
import { CurrentpageService } from './services/currentpage.service';
import { TypeService } from './services/type.service';
import { BaseFile } from './table/table/fileinfo';
import { ButtonStyle, TableColumn, TableConfig } from './table/table/tablecolumn';
import { SelConfig } from './typesel/selconfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  title = 'UI';
  selconfig!: SelConfig;

  isHandSet: boolean = false;

  tc: TableConfig<BaseFile> = {
    downloadUrl: `${environment.BaseUrl}${TableApi.downloadFile}`,
    columns: [
      { label: 'Name', valIndex: 'fileName', sort: true, format: item => item.fileName },
      { label: 'CreateTime', valIndex: 'addTime', sort: true, format: item => new Date(item.addTime).toLocaleDateString() },
      { label: 'Ext', valIndex: 'ext', sort: true },
      { label: 'Size', valIndex: 'size', sort: true, format: item => this.cur.resize(item.size) },
      {
        label: 'Actions', valIndex: 'actions',
        buttons: [
          {
            content: '下载', style: ButtonStyle.link, isHidden: true,
          }
        ],
      },
    ]
  };
  constructor(public typeServ: TypeService, public breakobs: BreakpointObserver, public cur: CurrentpageService) {
    this.breakobs.observe([Breakpoints.HandsetLandscape, Breakpoints.HandsetPortrait]).subscribe(res => {
      this.isHandSet = !res.matches;
    });
  }
  ngOnInit() {
    this.selconfig = {
      list: this.typeServ.getTypeList(),
      count: 0,
      selected: this.typeServ.getTypeList(),
    };
  }
  toggleEmit(item: MatSidenav, status: boolean) {
    switch (status) {
      case undefined: { item.toggle(); break; }
      case true: { if (item.opened) item.toggle(); break; }
      case false: { if (!item.opened) item.toggle(); break; }
    }
  }
}
