#
# Cookbook Name:: authentication_service
# Recipe:: default
#
# Copyright (c) 2017 The Authors, All Rights Reserved.
include_recipe 'authentication_service::configuration'
include_recipe 'authentication_service::install'
