import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from 'src/environments/environment';
import { TableApi } from '../api/tableApi';
import { CurrentpageService } from '../services/currentpage.service';
import { BaseFile, Dir, SizeType } from './table/fileinfo';
import { ButtonStyle, TableColumn } from './table/tablecolumn';


@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.sass']
})
export class TableComponent implements OnInit {

  btnStyle = ButtonStyle;
  @ViewChild(MatSort) sort: MatSort;

  styleBtn: string = 'mat-button';

  /** 表格的列配置 */
  @Input() column: TableColumn<BaseFile>[] = [
    { label: 'Name', valIndex: 'fileName', sort: true, format: item => item.fileName },
    { label: 'CreateTime', valIndex: 'addTime', sort: true, format: item => new Date(item.addTime).toLocaleDateString() },
    { label: 'Ext', valIndex: 'ext', sort: true },
    { label: 'Size', valIndex: 'size', sort: true, format: item => this.resize(item.size) },
    {
      label: 'Actions', valIndex: 'actions',
      buttons: [
        {
          content: '下载', style: ButtonStyle.link, url: item => {
            return `${environment.BaseUrl}${TableApi.downloadFile}/${item.id}`;
          }
        }
      ],
    },
  ];

  resize(size: number) {
    if (this.getSize(size, SizeType.B)) {
      return this.getSize(size, SizeType.B) + 'B';
    }
    if (this.getSize(size, SizeType.K)) {
      return this.getSize(size, SizeType.K) + 'K';
    }
    if (this.getSize(size, SizeType.M)) {
      return this.getSize(size, SizeType.M) + 'M';
    }
    if (this.getSize(size, SizeType.G)) {
      return this.getSize(size, SizeType.G) + 'G';
    }
    return 0;
  }

  getSize(size: number, type: SizeType): string | boolean {
    var result = size / type;
    return result > 1 && result < 1024 ? result.toFixed(2) : false;
  }

  tableDir: Dir;

  displayedColumns: string[] = [];
  dataSource: MatTableDataSource<BaseFile> = new MatTableDataSource([]);

  constructor(public http: HttpClient, public cur: CurrentpageService) { }
  ngOnInit(): void {
    this.displayedColumns = this.column.map(item => item.valIndex);
    this.http.get<Dir>(`${environment.BaseUrl}${TableApi.getRoot}`).subscribe((res: Dir) => {
      this.resetTableData(res);
    });
  };
  doubleClick(dir: Dir) {
    this.http.get<Dir>(`${environment.BaseUrl}${TableApi.getDirInfo}`, { params: { id: dir.id } }).subscribe(res => {
      this.resetTableData(res);
    });
    // this.tableDir.dirs.push(dir);
  }

  resetTableData(data: Dir): void {
    this.tableDir = data;
    this.cur.add(this.tableDir);
    this.justSetableData(data);
  };

  justSetableData(data: Dir): void {
    this.cur.getCurPageSub().subscribe(res => {
      this.dataSource.data = (res.dirs as BaseFile[]).concat((res.files as BaseFile[]));
      this.dataSource.sort = this.sort;
    });
  };

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  format(item: BaseFile, col: TableColumn<BaseFile>): string {
    return !!col.format ? col.format(item) : item[col.valIndex];
  }
};
