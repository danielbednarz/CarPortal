import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Message } from '../models/message';
import { PaginationResult } from '../models/paginaton';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getMessages(pageNumber, pageSize, container) {
    let params = this.getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);

    return this.getPaginatedResult<Message[]>(this.baseUrl + 'messages', params);
  }

  private getPaginatedResult<T>(url, params) {
    const paginatedResult: PaginationResult<T> = new PaginationResult<T>();

    return this.http
      .get<T>(url, { observe: 'response', params })
      .pipe(
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
  }
}
