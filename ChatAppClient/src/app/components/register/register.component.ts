import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterModel } from '../../models/register.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerModel: RegisterModel = new RegisterModel();

  constructor(
    private http: HttpClient,
    private router: Router
  ){}

  setImage(event:any){    
    this.registerModel.file = event.target.files[0];
  }

  register(){
    const formData = new FormData();
    formData.append("name", this.registerModel.name);
    formData.append("file", this.registerModel.file, this.registerModel.file.name);
    
    this.http.post("https://localhost:7123/api/Auth/Register", formData).subscribe(res=> {
      localStorage.setItem("accessToken", JSON.stringify(res));
      this.router.navigateByUrl("/");
    });
  }
}
