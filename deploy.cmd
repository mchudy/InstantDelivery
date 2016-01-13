@echo off
    echo Deploying the website...
    call InstantDelivery.Service.deploy.cmd

    echo Deploying the API...
    call InstantDelivery.Web.deploy.cmd

