# **M.V.B**.
**Model View Binder** -  *A small and robust framework for awesome cross platform architectures*

#*Quick Start*

 - Install Mvb.Core package in all your projects (that use Mvb components).
	 - [Mvb.Core](https://www.nuget.org/packages/Mvb.Core/)
	
 - Install platform specific package in your UI Projects.
	 - *Xamarin Android:* [Mvb.Platform.Droid](https://www.nuget.org/packages/Mvb.Platform.Droid/)
	 - *Xamarin iOS:* [Mvb.Platform.iOS](https://www.nuget.org/packages/Mvb.Platform.iOS/)
	 - *Xamarin.Mac:*	Mvb.Platform.MacOs
	 - *Universal Windows 10:* [Mvb.Platform.Win.Uwp](https://www.nuget.org/packages/Mvb.Platform.UWP/) 
	 - *Windows WPF* [Mvb.Platform.Win.Wpf](https://www.nuget.org/packages/Mvb.Platform.WPF/)
	 - *Windows Forms:* [Mvb.Platform.Win.WinForms](https://www.nuget.org/packages/Mvb.Platform.WinForms)

At the entry point of your platform specific project simply call:
	
	MvbPlatform.Init();

For Windows Forms project call:

	Mvb.Core.Mvb.NullInit();

##Documentation
[https://github.com/markjackmilian/MVB/wiki](https://github.com/markjackmilian/MVB/wiki)


##What is this (shortly)?
This is a small framework that guides you through the creation of cross platform applications (thanks to Xamarin ) developing a release platform agnostic high-level layer (Binders Layer) .

The *Binders Layer* can be thought as an application without interface composed of many small components (*Binder*) with limited liability .

The binders (*type MvbBase* ) expose simple APIs that allow you to connect the top final platform specific application layer ( UI ).

**See [WIKI](https://github.com/markjackmilian/MVB/wiki) section for all the necessary documentation .**

##Follow Me

 - Twitter: [@markjackmilian](https://twitter.com/markjackmilian)
 - MyBlog: [markjackmilian.net](http://markjackmilian.net/blog)
 - Linkedin: [linkedin](https://www.linkedin.com/in/marco-giacomo-milani)
