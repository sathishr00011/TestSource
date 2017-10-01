#
# Authors:: Sathishkumar R & Vinoth M
# Cookbook Name:: authentication_service
# Recipe:: install
#
# Copyright (c) 2016, Tesco, All Rights Reserved.

#create deployment folder if not exists

directory node['authentication_service']['install_location'] do
  action :create
end

#Copy msi's local folder

node['authentication_service']['artifacts'].each do |artifact|
remote_file artifact['download_path'] do
  source artifact['dfspath']
  not_if {::File.exists?(artifact['download_path'])}
end
end

#install msi
node['authentication_service']['artifacts'].each do |artifacts|
windows_package 'install msi' do
  source artifacts ['download_path']
  action :install
  end
end

# Create directory
directory node['authentication_service']['deployment_folder'] do
  action :create
  recursive true
  #rights :full_control, "dotcom\jv37", :applies_to_children => true
end

#copy artifacts to deployment folder
node['authentication_service']['components'].each do |component|
	source_folder = node['authentication_service']['source_folder'] + component
	Dir["#{source_folder}/**/*"].each do |curr_path|
		substring = curr_path.dup
		substring.gsub! source_folder, ""
		deployment_path = node['authentication_service']['deployment_folder'] + substring
		# Chef::Log.warn(deployment_path)
		# Chef::Log.info(curr_path)

		file deployment_path do
	  		content lazy {IO.read(curr_path)}
	  		action  :create
		end if File.file?(curr_path)

		directory deployment_path do
			action :create
		end if File.directory?(curr_path)

	end
end
