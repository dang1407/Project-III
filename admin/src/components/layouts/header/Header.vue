<template>
  <header class="flex-center">
    <div class="header__home--box">
      <div class="icons__container">
        <span class="material-symbols-outlined"> home </span>
      </div>
    </div>

    <div class="header__navbar--box">
      <!-- Navbar -->
      <nav class="flex">
        <div class="nav__item"><a href="">Tổng quan</a></div>
        <div class="nav__item">
          <router-link to="/employee">Nhân viên</router-link>
        </div>
        <div class="nav__item"><a href="">Sản phẩm</a></div>
      </nav>

      <!-- Header Account Box -->
      <div class="header__acc--box flex-center">
        <!-- <div class="relative">
          <div class="user__thumnail">
            <img src="@/assets/imgs/anhthe.jpg" alt="" />
          </div>
          <div class="droplist__container top-[100%]">
            <w-list :items="items" hover>
              <template #item="{ item }">
                <div class="droplist__item">
                  {{ item.label }}
                </div>
              </template>
            </w-list>
          </div>
        </div> -->
        <Menu :datas="menu" menu-position="top-[100%] right-[4px]">
          <template #menu_target>
            <div class="user__thumnail">
              <img src="@/assets/imgs/anhthe.jpg" alt="" />
            </div>
          </template>
          <template #menu_item="{ data }">
            {{ data.label }}
          </template>
        </Menu>
      </div>
    </div>
    <!-- </div> -->
  </header>
</template>

<script setup>
import "./header.css";
import { ref } from "vue";
import List from "../../base/list/List.vue";
import Menu from "../../base/menu/Menu.vue";
const props = defineProps({});
const menu = ref([
  {
    id: 1,
    label: "Hồ sơ của tôi",
  },
  {
    id: 2,
    label: "Đổi mật khẩu",
  },
  {
    id: 3,
    label: "Đăng xuất",
  },
]);
</script>

<style scoped>
@import url("./header.css");
</style>
