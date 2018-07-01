#!/bin/bash
az group create --name myResourceGroup --location eastus
az container create --resource-group myResourceGroup --name myContainerGroup -f deploy-azure.yml