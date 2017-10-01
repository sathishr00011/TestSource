# Cookbook Name:: authentication_service
# Recipe:: configuration
#
# Copyright (c) 2016, Tesco, All Rights Reserved.

#Use vault to update cookbook
#require 'chef-vault'

if node['authentication_service']['region'] == 'ce'
template node['authentication_service']['web_config_path'] do
  source "#{node.chef_environment}/#{node['authentication_service']['region']}/web.config.erb"
  #source "#{node['authentication_service']['chef_environment']}/web.config.erb"
  action :create
end
end

if node['authentication_service']['region'] == 'ap'
template node['authentication_service']['web_config_path'] do
  source "#{node.chef_environment}/#{node['authentication_service']['region']}/web.config.erb"
action :create
end
end
