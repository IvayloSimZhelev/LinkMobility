import { Injectable } from "@angular/core";
import { TreeNode } from "primeng/api";
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class NodeService {

    constructor(private http: HttpClient) {}

    getFilesystem() {
      return this.http.get<any>('assets/fileforsystem.json');
    }
}
