import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
name: string = "";

constructor(private http: HttpClient,
  private router: Router
){

}
login(){
  this.http.get("https://localhost:7123/api/Auth/Login?name=" + this.name).subscribe(res=> {
    localStorage.setItem("accessToken", JSON.stringify(res));
    this.router.navigateByUrl("/");
  });
}
}
