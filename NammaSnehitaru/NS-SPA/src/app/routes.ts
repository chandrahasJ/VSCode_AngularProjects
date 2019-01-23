import { Route } from '@angular/compiler/src/core';
import { HomeComponent } from './home/home.component';
import { MemberlistsComponent } from './memberlists/memberlists.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { Routes } from '@angular/router';
import { AuthGuard } from './_Guards/auth.guard';


export const appRoutes: Routes = [
    {path : '', component : HomeComponent},
    {
        path : '',
        runGuardsAndResolvers : 'always',
        canActivate : [AuthGuard],
        children : [
            {path : 'members', component : MemberlistsComponent},
            {path : 'lists', component : ListsComponent},
            {path : 'messages', component : MessagesComponent}
        ]
    },
    {path : '**', redirectTo : '', pathMatch : 'full'}
];
