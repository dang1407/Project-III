<template>
  <div
    class="flex items-center relative"
    :style="{
      width: `${parseInt(size.substring(0, size.length - 2)) + 1}px`,
      height: size,
    }"
  >
    <div class="absolute top-0 left-0">
      <div
        class="flex justify-center items-center rounded-[50%] border-[2px] border-[#089740]"
        :style="{ width: size, height: size }"
      >
        <div
          class="bg-[#089740] rounded-[50%]"
          :style="{
            width: `${parseInt(size.substring(0, size.length - 2)) - 8}px`,
            height: `${parseInt(size.substring(0, size.length - 2)) - 8}px`,
          }"
        ></div>
      </div>
    </div>

    <div
      :style="{ width: size, height: size }"
      class="absolute top-0 left-0 flex items-center justify-center"
    >
      <input
        @input="$emit('update:modelValue', $event.target.value)"
        ref="radioInputRef"
        class="dradio__input"
        type="radio"
        :value="value"
        :name="name"
        :style="{ width: size, height: size }"
      />
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
defineEmits(["update:modelValue"]);
const props = defineProps({
  name: {
    type: String,
  },
  size: {
    type: String,
    default: "16px",
  },
  modelValue: {},
  value: {},
});

const radioInputRef = ref();

onMounted(() => {
  if (props.modelValue == props.value) {
    radioInputRef.value.checked = true;
  }
});
</script>

<style lang="scss" scoped>
.dradio__input:checked {
  opacity: 0;
}

.dradio__input:focus-visible {
  background-color: rgba(0, 0, 0, 0.4);
  outline: 2px solid var(--button-bg-color);
  // outline-style: groove;
  // border-radius: 50%;
  // box-shadow: 0 2px 3px var(--button-bg-color);
}
</style>
