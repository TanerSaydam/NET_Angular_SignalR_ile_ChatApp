# .NET, Angular ve SignalR ile Chat Uygulaması Eğitimi
Arkadaşlar merhaba!

Bu eğitim ile SignalR kullanarak canlı chat yapmak isteyenlerin örnek alabileceği bir proje hazırladım.

Eğitim başında kopyala yapıştır ile aldığım dizayn kodları aşağıdadır.

Repoyu yıldızlarayak destek vermeyi unutmayın

- **Youtube videosu**


İyi eğitimler.


- **HTML**
```html
<div class="container">
  <div class="row clearfix">
    <div>
      <h1 class="alert alert-dark text-center mt-2">TS ChatAPP</h1>
    </div>
      <div class="col-lg-12 mt-2">
          <div class="card chat-app">
              <div id="plist" class="people-list">
                  <div class="input-group" style="position: relative;">                      
                      <input type="text" class="form-control" placeholder="Search..." style="padding-left:35px;">
                      <i class="fa fa-search" style="position: absolute; top:10px; left: 15px;"></i>
                  </div>
                  <ul class="list-unstyled chat-list mt-2 mb-0">
                    @for(user of users; track user){
                      <li class="clearfix" [ngClass]="selectedUserId == user.id ? 'active' : ''" (click)="changeUser(user)">
                          <img src="https://bootdey.com/img/Content/avatar/{{user.avatar}}" alt="avatar">
                          <div class="about">
                              <div class="name">{{user.name}}</div>
                              <div class="status"> <i class="fa fa-circle" [ngClass]="user.status === 'online' ? 'online' : 'offline'"></i> {{user.status}} </div>                                            
                          </div>
                      </li>
                    }                      
                  </ul>
              </div>
              @if(selectedUserId){
                <div class="chat">
                  <div class="chat-header clearfix">
                      <div class="row">
                          <div class="col-lg-6">
                              <a href="javascript:void(0);" data-toggle="modal" data-target="#view_info">
                                  <img src="https://bootdey.com/img/Content/avatar/{{selectedUser.avatar}}" alt="avatar">
                              </a>
                              <div class="chat-about">
                                  <h6 class="m-b-0">{{selectedUser.name}}</h6>
                                  <small>{{selectedUser.status}}</small>
                              </div>
                          </div>                         
                      </div>
                  </div>
                  <div class="chat-history" style="height: 600px;">
                      <ul class="m-b-0">
                        @for(chat of chats; track chat){
                          @if(selectedUserId != chat.userId){
                            <li class="clearfix d-flex" style="flex-direction: column; width: 100%; align-items:flex-end;">
                              <div class="message-data">                                
                                  <span class="message-data-time">{{chat.date}}</span>                                 
                              </div>
                              <div class="message other-message"> {{chat.message}} </div>
                          </li>
                          }@else {
                            <li class="clearfix">
                              <div class="message-data">
                                  <span class="message-data-time">{{chat.date}}</span>
                              </div>
                              <div class="message my-message">{{chat.message}}</div>                                    
                          </li>
                          }                        
                        }
                      </ul>
                  </div>
                  <div class="chat-message clearfix">
                      <div class="input-group mb-0">
                          <div class="input-group-prepend">
                              <span class="input-group-text"><i class="fa fa-send"></i></span>
                          </div>
                          <input type="text" class="form-control" placeholder="Enter text here..." style="height: 30px;">
                      </div>
                  </div>
              </div>
              }@else {
                <div class="chat">
                  <div class="chat-header clearfix">
                      <div class="row">
                          <div class="col-lg-6">
                              <a href="javascript:void(0);" data-toggle="modal" data-target="#view_info">
                                  <img src="https://bootdey.com/img/Content/avatar/avatar2.png" alt="avatar">
                              </a>
                              <div class="chat-about">
                                  <h6 class="m-b-0">Aiden Chavez</h6>
                                  <small>Last seen: 2 hours ago</small>
                              </div>
                          </div>                         
                      </div>
                  </div>
                  <div class="chat-history">
                      <ul class="m-b-0">
                          <li class="clearfix">
                              <div class="message-data text-right">
                                  <span class="message-data-time">10:10 AM, Today</span>
                                  <img src="https://bootdey.com/img/Content/avatar/avatar7.png" alt="avatar">
                              </div>
                              <div class="message other-message float-right"> Hi Aiden, how are you? How is the project coming along? </div>
                          </li>
                          <li class="clearfix">
                              <div class="message-data">
                                  <span class="message-data-time">10:12 AM, Today</span>
                              </div>
                              <div class="message my-message">Are we meeting today?</div>                                    
                          </li>                               
                          <li class="clearfix">
                              <div class="message-data">
                                  <span class="message-data-time">10:15 AM, Today</span>
                              </div>
                              <div class="message my-message">Project has been already finished and I have results to show you.</div>
                          </li>
                      </ul>
                  </div>
                  <div class="chat-message clearfix">
                      <div class="input-group mb-0">
                          <div class="input-group-prepend">
                              <span class="input-group-text"><i class="fa fa-send"></i></span>
                          </div>
                          <input type="text" class="form-control" placeholder="Enter text here..." style="height: 30px;">
                      </div>
                  </div>
              </div>
              }
              
          </div>
      </div>
  </div>
  </div>
```

- **TS**
```ts
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  users = Users;
  chats = Chats;
  selectedUserId: string = "1";
  selectedUser: UserModel = {
    id: "1",
    name: "Vincent Porter",
    status: "left 7 min ago",
    avatar: "avatar1.png"
  };


  changeUser(user: UserModel){
    this.selectedUserId = user.id;
    this.selectedUser = user;

    this.chats = Chats.filter(p=> p.toUserId == user.id && p.userId == "0" || p.userId == user.id && p.toUserId == "0");
  }

}

export class UserModel{
  id:string = "";
  name: string = "";
  status: string = "";
  avatar: string = "";
}

export const Users: UserModel[] = [
  {
    id: "1",
    name: "Vincent Porter",
    status: "left 7 min ago",
    avatar: "avatar1.png"
  },
  {
    id: "2",
    name: "Aiden Chavez",
    status: "online",
    avatar: "avatar3.png"
  },
  {
    id: "3",
    name: "Christian Kelly",
    status: "offline since oct 28",
    avatar: "avatar3.png"
  }
]

export class ChatModel{
  userId: string = "";
  toUserId: string = "";
  date: string  ="";
  message: string = "";
}

export const Chats: ChatModel[] = [
  {
    userId: "0",
    toUserId: "1",
    date: new Date().toString(),
    message: "Hi Aiden, how are you? How is the project coming along?"
  },
  {
    userId: "1",
    toUserId: "0",
    date: new Date().toString(),
    message: "Are we meeting today?"
  },
  {
    userId: "1",
    toUserId: "0",
    date: new Date().toString(),
    message: "Project has been already finished and I have results to show you."
  }
]
```

- **CSS**
```css
body{
    background-color: #f4f7f6;
    margin-top:20px;
}
.card {
    background: #fff;
    transition: .5s;
    border: 0;
    margin-bottom: 30px;
    border-radius: .55rem;
    position: relative;
    width: 100%;
    box-shadow: 0 1px 2px 0 rgb(0 0 0 / 10%);
}
.chat-app .people-list {
    width: 280px;
    position: absolute;
    left: 0;
    top: 0;
    padding: 20px;
    z-index: 7
}

.chat-app .chat {
    margin-left: 280px;
    border-left: 1px solid #eaeaea
}

.people-list {
    -moz-transition: .5s;
    -o-transition: .5s;
    -webkit-transition: .5s;
    transition: .5s
}

.people-list .chat-list li {
    padding: 10px 15px;
    list-style: none;
    border-radius: 3px
}

.people-list .chat-list li:hover {
    background: #efefef;
    cursor: pointer
}

.people-list .chat-list li.active {
    background: #efefef
}

.people-list .chat-list li .name {
    font-size: 15px
}

.people-list .chat-list img {
    width: 45px;
    border-radius: 50%
}

.people-list img {
    float: left;
    border-radius: 50%
}

.people-list .about {
    float: left;
    padding-left: 8px
}

.people-list .status {
    color: #999;
    font-size: 13px
}

.chat .chat-header {
    padding: 15px 20px;
    border-bottom: 2px solid #f4f7f6
}

.chat .chat-header img {
    float: left;
    border-radius: 40px;
    width: 40px
}

.chat .chat-header .chat-about {
    float: left;
    padding-left: 10px
}

.chat .chat-history {
    padding: 20px;
    border-bottom: 2px solid #fff
}

.chat .chat-history ul {
    padding: 0
}

.chat .chat-history ul li {
    list-style: none;
    margin-bottom: 30px
}

.chat .chat-history ul li:last-child {
    margin-bottom: 0px
}

.chat .chat-history .message-data {
    margin-bottom: 15px
}

.chat .chat-history .message-data img {
    border-radius: 40px;
    width: 40px
}

.chat .chat-history .message-data-time {
    color: #434651;
    padding-left: 6px
}

.chat .chat-history .message {
    color: #444;
    padding: 18px 20px;
    line-height: 26px;
    font-size: 16px;
    border-radius: 7px;
    display: inline-block;
    position: relative
}

.chat .chat-history .message:after {
    bottom: 100%;
    left: 7%;
    border: solid transparent;
    content: " ";
    height: 0;
    width: 0;
    position: absolute;
    pointer-events: none;
    border-bottom-color: #fff;
    border-width: 10px;
    margin-left: -10px
}

.chat .chat-history .my-message {
    background: #efefef
}

.chat .chat-history .my-message:after {
    bottom: 100%;
    left: 30px;
    border: solid transparent;
    content: " ";
    height: 0;
    width: 0;
    position: absolute;
    pointer-events: none;
    border-bottom-color: #efefef;
    border-width: 10px;
    margin-left: -10px
}

.chat .chat-history .other-message {
    background: #e8f1f3;
    text-align: right
}

.chat .chat-history .other-message:after {
    border-bottom-color: #e8f1f3;
    left: 93%
}

.chat .chat-message {
    padding: 20px
}

.online,
.offline,
.me {
    margin-right: 2px;
    font-size: 8px;
    vertical-align: middle
}

.online {
    color: #86c541
}

.offline {
    color: #e47297
}

.me {
    color: #1d8ecd
}

.float-right {
    float: right
}

.clearfix:after {
    visibility: hidden;
    display: block;
    font-size: 0;
    content: " ";
    clear: both;
    height: 0
}

@media only screen and (max-width: 767px) {
    .chat-app .people-list {
        height: 465px;
        width: 100%;
        overflow-x: auto;
        background: #fff;
        left: -400px;
        display: none
    }
    .chat-app .people-list.open {
        left: 0
    }
    .chat-app .chat {
        margin: 0
    }
    .chat-app .chat .chat-header {
        border-radius: 0.55rem 0.55rem 0 0
    }
    .chat-app .chat-history {
        height: 300px;
        overflow-x: auto
    }
}

@media only screen and (min-width: 768px) and (max-width: 992px) {
    .chat-app .chat-list {
        height: 650px;
        overflow-x: auto
    }
    .chat-app .chat-history {
        height: 600px;
        overflow-x: auto
    }
}

@media only screen and (min-device-width: 768px) and (max-device-width: 1024px) and (orientation: landscape) and (-webkit-min-device-pixel-ratio: 1) {
    .chat-app .chat-list {
        height: 480px;
        overflow-x: auto
    }
    .chat-app .chat-history {
        height: calc(100vh - 350px);
        overflow-x: auto
    }
}
```

- **index.HTML**
```html
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <title>ChatAppClient</title>
  <base href="/">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="icon" type="image/x-icon" href="favicon.ico">
  <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</head>
<body>
  <app-root></app-root>
</body>
</html>
```