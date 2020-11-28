import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CertificacaoService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(`${environment.endpoint}/certificacao`);
  }

  add(entity: any) {
    return this.http.post(`${environment.endpoint}/certificacao`, entity);
  }

  update(id: string, entity: any) {
    return this.http.put(`${environment.endpoint}/certificacao/${id}`, entity);
  }

  delete(id: string) {
    return this.http.delete(`${environment.endpoint}/certificacao/${id}`);
  }
}
