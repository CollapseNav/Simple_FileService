import { MatButtonModule } from "@angular/material/button";
import { MatRipple } from "@angular/material/core";

export interface TableColumn<T> {
  label: string;
  valIndex?: string;
  format?: (item: T) => any;
  default?: any;
  sort?: boolean;
  select?: boolean;
  buttons?: TableColumnButton<T>[];
}

export interface TableColumnButton<T> {
  content: string;
  style?: ButtonStyle;
  url?: (item: T) => any;
  color?: string;
  click?: (item: T) => any;
}


export enum ButtonStyle {
  link, raised, stroked, flat, icon, fab, mini
}
