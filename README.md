# **M.V.B**.
**Model View Binder** -  *A small and robust framework for awesome cross platform architectures*

#*Quick Start*

 - Install Mvb.Core package in all your projects (that use Mvb components).
	 - [Mvb.Core Nuget](https://www.nuget.org/packages/Mvb.Core/0.0.8)
	
 - Install platform specific package in your UI Projects.
	 - *Xamarin Android:* Mvb.Platform.Droid
	 - *Xamarin iOS:* Mvb.Platform.iOS
	 - *Xamarin.Mac:*	Mvb.Platform.MacOs
	 - *Universal Windows 10:* Mvb.Platform.Win.Uwp 
	 - *Windows WPF* Mvb.Platform.Win.Wpf
	 - *Windows Forms:* Mvb.Platform.Win.WinForms

At the entry point of your platform specific project simply call:
	
	MvbPlatform.Init();

For Windows Forms project call:

	Mvb.Core.Mvb.NullInit();

##Documentation
[https://github.com/markjackmilian/MVB/wiki](https://github.com/markjackmilian/MVB/wiki)


##What is this (shortly)?
Questo è un piccolo framework che ti guida nella creazione di applicazioni cross platform (grazie a Xamarin) permettendoti di creare uno strato di alto livello agnostico rispetto alla piattaforma di sviluppo.

This is a small framework that guides you through the creation of cross platform applications (thanks to Xamarin ) developing a release platform agnostic high-level layer (Binders Layer) .


Il Binders Layer può essere immaginato come un'applicazione senza interfaccia composta da tanti piccoli componenti con circoscritte responsabilità . 

The *Binders Layer* can be thought as an application without interface composed of many small components (*Binder*) with limited liability .

I binders (di tipo MvbBase) espongono intuitive API che permettono di collegare l'ultimo strato applicativo (UI) specifico della piattaforma finale.

The binders (*type MvbBase* ) expose simple APIs that allow you to connect the top final platform specific application layer ( UI ).

>Nel dettaglio
 - Consultare la sezione [WIKI](https://github.com/markjackmilian/MVB/wiki) per tutta la documentazione necessaria.
 - Una semplice applicazione di dimostrazione può essere trovata QUI.

>In depth
- See WIKI section for all the necessary documentation .
 - A more detailed explanation can be found HERE .
 - A simple demonstration application can be found HERE .


##Follow Me

 - Twitter: [@markjackmilian](https://twitter.com/markjackmilian)
 - MyBlog: [markjackmilian.net](http://markjackmilian.net/blog)
 - Linkedin: [linkedin](https://www.linkedin.com/in/marco-giacomo-milani)
