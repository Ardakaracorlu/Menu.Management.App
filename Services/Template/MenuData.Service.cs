public class MenuDataService
{
    private List<MainMenuItems> MenuData = new List<MainMenuItems>()
    {

        new MainMenuItems(
            menuTitle: "MAIN"
        ),
        new MainMenuItems (
            path: "/home",
            type: "link",
            icon: "bx bx-home",
            title: "AnaSayfa",
            selected: false,
            active: false,
            dirChange: false
        ),
           new MainMenuItems (
            path: "/getstarted",
            type: "link",
            icon: "bx bx-rocket",
            title: "Hadi Baþlayalým",
            selected: false,
            active: false,
            dirChange: false
        ),
        //new MainMenuItems(
        //    type: "sub",
        //    title: "Dashboards",
        //    icon: "bx bx-home",
        //    badgeValue: "12",
        //    badgeClass: "bg-warning-transparent",
        //    selected: false,
        //    active: false,
        //    dirChange: false,
        //    children: new MainMenuItems[]
        //    {
        //      new MainMenuItems (
        //            path: "/home",
        //            type: "link",
        //            title: "CRM",
        //            selected: false,
        //            active: false,
        //            dirChange: false
        //        ),
        //    }
        //),
        //new MainMenuItems (
        //    type: "sub",
        //    title: "Nested Menu",
        //    icon: "bx bx-layer",
        //    selected: false,
        //    active: false,
        //    dirChange: false,
        //    children: new MainMenuItems[]
        //    {
        //        new MainMenuItems (
        //            path: "",
        //            type: "empty",
        //            title: "Nested-1",
        //            selected: false,
        //            active: false,
        //            dirChange: false
        //        ),
        //        new MainMenuItems (
        //            type: "sub",
        //            title: "Nested-2",
        //            selected: false,
        //            active: false,
        //            dirChange: false,
        //            children: new MainMenuItems[]
        //            {
        //                new MainMenuItems (
        //                    path: "",
        //                    type: "empty",
        //                    title: "Nested-2-1",
        //                    selected: false,
        //                    active: false,
        //                    dirChange: false
        //                ),
        //                new MainMenuItems (
        //                    type: "sub",
        //                    title: "Nested-2-2",
        //                    selected: false,
        //                    active: false,
        //                    dirChange: false,
        //                    children: new MainMenuItems[]
        //                    {
        //                        new MainMenuItems (
        //                            path: "",
        //                            type: "empty",
        //                            title: "Nested-2-2-1",
        //                            selected: false,
        //                            active: false,
        //                            dirChange: false
        //                        ),
        //                        new MainMenuItems (
        //                            path: "",
        //                            type: "empty",
        //                            title: "Nested-2-2-2",
        //                            selected: false,
        //                            active: false,
        //                            dirChange: false
        //                        )
        //                    }
        //                )
        //            }
        //        )
        //    }
        //),

    };

    public List<MainMenuItems> GetMenuData()
    {
        return MenuData;
    }
}
