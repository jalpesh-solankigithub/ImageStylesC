import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) {}

	getImageVersions(url: string) {
	return this.http.get<any>(`YOUR_API_ENDPOINT_HERE?blobUrl=${encodeURIComponent(url)}`);
}
}
