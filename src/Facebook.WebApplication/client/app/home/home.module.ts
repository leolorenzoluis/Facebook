import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HomeRoutes } from './home.routes';
import { HomeComponent } from './home.component';
import { MdlModule } from 'angular2-mdl';

import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        MdlModule,
        HomeRoutes,
        SharedModule
    ],

    declarations: [
        HomeComponent
    ],

    exports: [
        HomeComponent
    ]
})

export class HomeModule { }